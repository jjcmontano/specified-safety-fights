/* eslint-disable react/forbid-prop-types */
import PropTypes from 'prop-types';
import React, { createContext, useContext, useReducer } from 'react';

export const ReportsContext = createContext();

const reducer = (state, action) => {
    switch (action.type) {
    case "SET_REPORTS":
        return {
            ...state,
            reports: action.payload,
            reportsLoading: false,
        };
    case "ADD_REPORT":
        return {
            ...state,
            reports: state.reports.concat(action.payload),
            reportsLoading: false,
        };
    case 'SET_REPORT_ERROR':
        return {
            ...state,
            reportError: action.payload,
            reportsLoading: false,
        };
    case 'SET_SUMMARY':
        return {
            ...state,
            summary: action.payload,
            summaryLoading: false,
        }
    case 'SET_SUMMARY_ERROR':
        return {
            ...state,
            summaryError: action.payload,
            summaryLoading: false,
        };
    case 'SET_REPORTS_LOADING':
        return {
            ...state,
            reportsLoading: true,
        }
    case 'SET_SUMMARY_LOADING':
        return {
            ...state,
            summaryLoading: true,
        }
    case 'SHOW_SUMMARY':
        return {
            ...state,
            showSummary: true,
        }
    case 'HIDE_SUMMARY':
        return {
            ...state,
            showSummary: false,
            summary: null,
        }
    default:
        return state;
    }
};

export function ReportsProvider({ initialState, children }) {
    return (<ReportsContext.Provider value={useReducer(reducer, initialState)}>
        {children}
    </ReportsContext.Provider>);
}

ReportsProvider.defaultProps ={
    initialState: {},
    children: []
}

ReportsProvider.propTypes = {
    initialState: PropTypes.object,
    children: PropTypes.array,
}

export const useReports = () => useContext(ReportsContext);