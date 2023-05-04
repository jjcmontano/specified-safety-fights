import { Error } from "@mui/icons-material";
import { Button, Dialog, DialogActions, DialogContent, DialogTitle, Skeleton, Slide, TextField, Typography } from "@mui/material";
import React, { useContext, useEffect } from 'react';
import SummaryApi from "../api/SummaryApi";
import { ReportsContext } from "../contexts/ReportsStore";

// eslint-disable-next-line react/prop-types, react/jsx-props-no-spreading
const Transition = React.forwardRef(({ref, ...props}) => <Slide direction="up" ref={ref} {...props} />)

function SummaryDialog() {
    const [{reports, selectedReportId, summary, summaryLoading, summaryError, showSummary}, dispatch] = useContext(ReportsContext);

    const selectedReport = selectedReportId ? reports.find(r => r.id === selectedReportId) : null;

    const handleClose = () => dispatch({type: 'HIDE_SUMMARY'})

    useEffect(() => {
        const getSummary = async () => {
            try {
                dispatch({type: 'SET_SUMMARY_LOADING'});
                const summaryData = await SummaryApi.getSummary(selectedReportId);
                dispatch({type: 'SET_SUMMARY', payload: summaryData});
            } catch (error) {
                dispatch({type: 'SET_SUMMARY_ERROR', payload: error?.message ?? error ?? 'Unknown'});
            } finally {
                dispatch({type: 'SET_SUMMARY_DONE'});
            }
        }

        if (selectedReportId) {
            getSummary();
        }
    }, [selectedReportId])

    if (!showSummary || !selectedReport) {
        return null
    }

    return (
        <Dialog
            open={showSummary || !selectedReport}
            TransitionComponent={Transition}
            keepMounted
            onClose={handleClose}
            aria-describedby="alert-dialog-slide-description"
            maxWidth="lg"
            fullWidth
        >
            <DialogTitle>{selectedReport.name}</DialogTitle>
            <DialogContent>
                {summaryError && (<Typography color="error.main" variant="body1"><Error sx={{mr: 1}} />Failed to fetch report summary: {summaryError}</Typography>)}
                {summaryLoading ? <Skeleton variant="rounded" sx={{height: '400px'}} /> : (
                    <TextField
                        multiline
                        InputProps={{
                            fontSize: '0.5em'
                        }}
                        value={summary}
                        fullWidth
                    />
                )}
            </DialogContent>
            <DialogActions>
                <Button onClick={handleClose}>Close</Button>
            </DialogActions>
        </Dialog>
    );
}

export default SummaryDialog