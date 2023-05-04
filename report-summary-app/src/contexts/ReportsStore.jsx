/* eslint-disable react/prop-types */
import React, { createContext, useReducer } from "react";
import ReportsReducer from "./ReportsReducer";

const initialState = {
    reports: [],
    reportsLoading: false,
    reportsError: null,
    summary: null,
    summaryError: null,
    showSummary: false,
}

function ReportsStore({children}) {
    // const [state, dispatch] = useReducer(ReportsReducer, initialState);

    // const storeValue = useMemo(() => [state, dispatch], [state, dispatch]);

    return (
        <ReportsContext.Provider value={useReducer(ReportsReducer, initialState)}>
            {children}
        </ReportsContext.Provider>
    )
}

export const ReportsContext = createContext(initialState);
export default ReportsStore;