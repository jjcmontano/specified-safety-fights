import AxiosInstance from "./AxiosInstance";

const SummaryApi = {
    getSummary: async (reportRecordId) => (await AxiosInstance.get(`/Summary/${reportRecordId}`, { timeout: 20000 })).data
}

export default SummaryApi;