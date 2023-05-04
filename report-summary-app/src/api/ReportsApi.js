import AxiosInstance from "./AxiosInstance";

const ReportsApi = {
    getReports: async () => (await AxiosInstance.get('/Report')).data
}

export default ReportsApi;