const getErrorsAsString = (error) => {
  const errorMessage =
    error.response.status === 500
      ? error.response.data.error
      : error.response.data
          .map((er) => er.concat('\n'))
          .toString()
          .replace(',', '')

  return errorMessage
}

export default getErrorsAsString
