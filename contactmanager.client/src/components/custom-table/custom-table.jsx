import { ApiMethods } from '../../constants/api-methods'
import { ApiRoutes } from '../../constants/api-routes'
import { useSortableTable } from '../../useSortableTable'
import getErrorsAsString from '../../utilities/get-errors-as-string'
import makeRequestAsync from '../../utilities/make-request-async'
import axios from 'axios'

import { TableBody } from './table-body'
import { TableHead } from './table-head'

const CustomTable = ({
  data,
  columns,
  initialData,
  refreshContacts = null,
}) => {
  const handleRemoveContact = async (event, id) => {
    const signal = axios.CancelToken.source()

    try {
      await makeRequestAsync(
        ApiRoutes.removeContact,
        signal.token,
        id,
        ApiMethods.post
      )
      alert('Contact removed')
    } catch (error) {
      const errors = getErrorsAsString(error)
      alert(errors)
    } finally {
      refreshContacts?.()
    }
  }

  const handleUpdateContact = async (event, contactsData, contactId) => {
    const signal = axios.CancelToken.source()
    const contact = contactsData.filter((c) => c.id === contactId)[0]
    contact.married = contact.married.toLowerCase() === 'yes' ? true : false
    try {
      await makeRequestAsync(
        ApiRoutes.updateContact,
        signal.token,
        contact,
        ApiMethods.post
      )
      alert('Contact updated')
    } catch (error) {
      const errors = getErrorsAsString(error)
      alert(errors)
    } finally {
      refreshContacts?.()
    }
  }

  const [tableData, setTableData, handleSorting] = useSortableTable(
    data,
    initialData,
    columns
  )

  return (
    <>
      <h2>Contacts list</h2>
      <table className="table table-striped" aria-labelledby="tableLabel">
        <TableHead columns={columns} handleSorting={handleSorting} />
        <TableBody
          columns={columns}
          tableData={tableData}
          handleRemove={handleRemoveContact}
          handleUpdate={handleUpdateContact}
          setTableData={setTableData}
        />
      </table>
    </>
  )
}

export default CustomTable
