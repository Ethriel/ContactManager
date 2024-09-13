import axios from 'axios';
import { ApiRoutes } from '../constants/api-routes';

const makeRequestAsync = async (urlTail, cancelToken, postData = null, method = 'get', parameters = null) => {
    const requestUrl = ApiRoutes.root.concat(urlTail);
    axios.defaults.withCredentials = false;
    let axiosConfig = {};
    if (postData === null && parameters !== null) {
        axiosConfig = {
            method: method,
            params: parameters,
            cancelToken: cancelToken,
            url: requestUrl,
            headers: {
                "Content-Type": "application/json",
            }
        };
    }
    else {
        axiosConfig = {
            method: method,
            data: postData,
            cancelToken: cancelToken,
            url: requestUrl,
            headers: {
                "Content-Type": "application/json",
            }
        };
    }

    return await axios(axiosConfig);
};

export default makeRequestAsync;