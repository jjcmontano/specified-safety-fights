import {
    Card, CardActionArea, CardContent, CardHeader, Typography
} from '@mui/material';
import PropTypes from 'prop-types';
import React, { useContext } from 'react';
import { ReportsContext } from '../contexts/ReportsStore';

function ReportCard(props) {
    const { report } = props;
    const [, dispatch] = useContext(ReportsContext);

    const handleClick = () => dispatch({type: 'SELECT_REPORT', payload: report.id})

    return (
        <Card sx={(theme) => ({boxShadow: theme.shadows[2]})} onClick={handleClick}>
            <CardActionArea>
                <CardHeader
                    title={report.name}
                    subheader={`Year: ${report.reportYear}`}
                    titleTypographyProps={{ align: 'center' }}
                    subheaderTypographyProps={{
                        align: 'center',
                    }}
                    sx={{
                        backgroundColor: (theme) =>
                            theme.palette.mode === 'light'
                                ? theme.palette.grey[200]
                                : theme.palette.grey[700],
                    }}
                />
                <CardContent>
                    <Typography variant="body1">{`${report.reportCode} - ${report.reportSectorTitle} Report`}</Typography>
                </CardContent>
            </CardActionArea>
        </Card>
    );
}

ReportCard.propTypes = {
    report: PropTypes.shape({
        id: PropTypes.string,
        name: PropTypes.string,
        reportYear: PropTypes.number,
        reportSectorTitle: PropTypes.string,
        reportCode: PropTypes.string,
    }).isRequired,
}

export default ReportCard;