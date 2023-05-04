const ReportsReducer = (state, action) => {
    switch (action.type) {
    case "SET_REPORTS":
        return {
            ...state,
            reports: action.payload,
            reportsError: null,
            reportsLoading: false,
        };
    case "ADD_REPORT":
        return {
            ...state,
            reports: state.reports.concat(action.payload),
            reportsError: null,
            reportsLoading: false,
        };
    case 'SET_REPORTS_ERROR':
        return {
            ...state,
            reportsError: action.payload,
            reportsLoading: false,
        };
    case 'SET_REPORTS_DONE':
        return {
            ...state,
            reportsLoading: false,
        };
    case 'SET_SUMMARY_DONE':
        return {
            ...state,
            summaryLoading: false,
        };
    case 'SELECT_REPORT':
        return {
            ...state,
            selectedReportId: action.payload,
            showSummary: true,
        }
    case 'SET_SUMMARY':
        return {
            ...state,
            summary: action.payload,
            summaryError: null,
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
            reportsError: null,
            reportsLoading: true,
        }
    case 'SET_SUMMARY_LOADING':
        return {
            ...state,
            summaryError: null,
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
            selectedReportId: null,
            summaryLoading: true
        }
    default:
        return state;
    }
};

export default ReportsReducer;