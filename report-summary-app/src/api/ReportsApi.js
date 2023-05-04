import AxiosInstance from "./AxiosInstance";

const ReportsApi = {
    getReports: async () => (await AxiosInstance.get('/Report')).data,
    postReport: async (file) => {
        const formData = new FormData();
        formData.append('reportFile', file);

        return (await AxiosInstance.post(
            '/Report',
            formData, {
                headers: {
                    'Content-Type': 'multipart/form-data'
                }
            })).data;
    }
}

export default ReportsApi;