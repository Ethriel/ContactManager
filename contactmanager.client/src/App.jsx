import axios from 'axios'
import { useEffect, useState } from 'react'

import './App.css'
import CustomTable from './components/custom-table/custom-table'
import { FileUploader } from './components/file-uploader'
import { ApiMethods } from './constants/api-methods'
import { ApiRoutes } from './constants/api-routes'
import getErrorsAsString from './utilities/get-errors-as-string'
import makeRequestAsync from './utilities/make-request-async'

function App() {
  const populateContactsData = async () => {
    try {
      setLoading(true)
      const signal = axios.CancelToken.source()
      const response = await makeRequestAsync(
        ApiRoutes.listContacts,
        signal.token
      )

      if (response.status !== 204) {
        const apiResult = response.data
        const originalContacts = Array.from(apiResult.data)
        const mappedContacts = originalContacts.map((contact) => {
          return contact.married
            ? { ...contact, married: 'Yes' }
            : { ...contact, married: 'No' }
        })
        setInitialContacts(mappedContacts)
        setContacts(mappedContacts)
      }
    } catch (error) {
      //console.log(error);
      const errors = getErrorsAsString(error)
      alert(errors)
    } finally {
      setLoading(false)
    }
  }

  useEffect(() => {
    populateContactsData()
  }, [])

  const [loading, setLoading] = useState(false)
  const [contacts, setContacts] = useState()
  const [initialContacts, setInitialContacts] = useState()
  const [selectedFile, setSelectedFile] = useState({})
  const [fileSelected, setFileSelected] = useState(false)

  const onFileChange = (event, resetFile) => {
    const file = event.target.files[0]
    setSelectedFile(file)
    setFileSelected(true)
    resetFile?.()
  }

  const onFileUpload = async () => {
    const signal = axios.CancelToken.source()
    try {
      setLoading(true)
      const formData = new FormData()
      formData.set('csv', selectedFile, selectedFile.name)

      await makeRequestAsync(
        ApiRoutes.uploadCsv,
        signal.token,
        formData,
        ApiMethods.post
      )
      alert('Successfully got contacts from file')
    } catch (error) {
      const errors = getErrorsAsString(error)
      alert(errors)
    } finally {
      setLoading(false)
      setFileSelected(false)
      setSelectedFile(null)
      populateContactsData()
    }
  }

  const columns = [
    {
      label: 'Name',
      accessor: 'name',
      sortable: true,
      sortbyOrder: 'desc',
      editable: true,
    },
    {
      label: 'Date of birth',
      accessor: 'dateOfBirth',
      sortable: true,
      editable: true,
    },
    { label: 'Married', accessor: 'married', sortable: true, editable: true },
    { label: 'Phone', accessor: 'phone', sortable: true, editable: true },
    { label: 'Salary', accessor: 'salary', sortable: true, editable: true },
    { label: 'Update', accessor: 'update', sortable: false, editable: false },
    { label: 'Remove', accessor: 'remove', sortable: false, editable: false },
  ]

  return (
    <div>
      <h1>Contacts manager</h1>
      <div>
        <h2>Upload CSV</h2>
        <FileUploader
          fileSelected={fileSelected}
          onFileChange={onFileChange}
          onFileUpload={onFileUpload}
          multiple={false}
          accept={'.csv'}
        />
      </div>

      {loading && (
        <p>
          <em>Loading...</em>
        </p>
      )}
      {!loading && (contacts === undefined || contacts.length === 0) && (
        <p>
          <em>No contacts to show</em>
        </p>
      )}
      {!loading && contacts !== undefined && contacts.length !== 0 && (
        <CustomTable
          columns={columns}
          data={contacts}
          initialData={initialContacts}
          refreshContacts={populateContactsData}
        />
      )}
    </div>
  )
}

export default App
