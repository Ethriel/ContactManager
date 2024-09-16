import { ApiMethods } from '../constants/api-methods'
import { ApiRoutes } from '../constants/api-routes'
import axios from 'axios'

const makeRequestAsync = async (
  urlTail,
  cancelToken,
  postData = null,
  method = ApiMethods.get,
  parameters = null
) => {
  const requestUrl = ApiRoutes.root.concat(urlTail)
  axios.defaults.withCredentials = false
  const axiosConfig =
    (postData === null && parameters !== null)
      ? {
          method: method,
          params: parameters,
          cancelToken: cancelToken,
          url: requestUrl,
          headers: {
            'Content-Type': 'application/json',
          },
        }
      : {
          method: method,
          data: postData,
          cancelToken: cancelToken,
          url: requestUrl,
          headers: {
            'Content-Type': 'application/json',
          },
        }

  return await axios(axiosConfig)
}

export default makeRequestAsync
