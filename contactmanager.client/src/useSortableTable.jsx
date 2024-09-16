import isEqual from 'lodash.isequal'
import { useState } from 'react'

export const useSortableTable = (data = [], defaultData = [], columns = []) => {
  const getDefaultSorting = (defaultTableData, columns) => {
    const sorted = [...defaultTableData].sort((a, b) => {
      const filterColumn = columns.filter((column) => column.sortbyOrder)

      // Merge all array objects into single object and extract accessor and sortbyOrder keys
      let { accessor = 'id', sortbyOrder = 'asc' } = Object.assign(
        {},
        ...filterColumn
      )

      if (a[accessor] === null) return 1
      if (b[accessor] === null) return -1
      if (a[accessor] === null && b[accessor] === null) return 0

      const ascending = a[accessor]
        .toString()
        .localeCompare(b[accessor].toString(), 'en', {
          numeric: true,
        })

      return sortbyOrder === 'asc' ? ascending : -ascending
    })

    return traceEditable(sorted, defaultData)
  }

  const [tableData, setTableData] = useState(() =>
    getDefaultSorting(data, columns)
  )

  const handleSorting = (sortField, sortOrder) => {
    if (sortField) {
      const sorted = [...tableData].sort((a, b) => {
        if (a[sortField] === null) return 1
        if (b[sortField] === null) return -1
        if (a[sortField] === null && b[sortField] === null) return 0
        const returnValue = sortField.toLowerCase().includes('date')
          ? new Date(a[sortField]) - new Date(b[sortField])
          : a[sortField]
              .toString()
              .localeCompare(b[sortField].toString(), 'en', {
                numeric: true,
              })
        return returnValue * (sortOrder === 'asc' ? 1 : -1)
      })
      setTableData(traceEditable(sorted, defaultData))
    }
  }

  const handleSetData = (newData) => {
    setTableData(traceEditable(newData, defaultData))
  }

  return [tableData, handleSetData, handleSorting]
}

const traceEditable = (array = [], initialArray = []) => {
  const result = array.map((item) => {
    const initial = initialArray.find(
      (initialItem) => initialItem.id === item.id
    )
    if (!initial) return item

    return {
      ...item,
      isEdited: !isEqual(omitServiceFields(initial), omitServiceFields(item)),
    }
  })

  return result
}

const omitServiceFields = ({ isEdited, ...rest }) => rest
