/* eslint-disable react/prop-types */
import React, { createContext, useMemo, useReducer } from "react";
import ReportsReducer from "./ReportsReducer";

const initialState = {
    reports: [
        {
            id: 'foo',
            name: 'foo',
            year: 1,
            reportSectorTitle: 'foo',
            reportCode: 'foo',
        },
        {
            id: 'bar',
            name: 'foo',
            year: 3,
            reportSectorTitle: 'foo',
            reportCode: 'foo',
        },
    ],
    reportsLoading: false,
    reportsError: null,
    summary: null,
    summaryError: null,
    showSummary: false,
}

function ReportsStore({children}) {
    const [state, dispatch] = useReducer(ReportsReducer, initialState);

    const storeValue = useMemo(() => [state, dispatch], [state, dispatch]);

    return (
        <ReportsContext.Provider value={storeValue}>
            {children}
        </ReportsContext.Provider>
    )
}

export const ReportsContext = createContext(initialState);
export default ReportsStore;