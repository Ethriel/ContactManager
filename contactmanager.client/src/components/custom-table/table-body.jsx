import { useState } from 'react'

export const TableBody = ({
  tableData,
  columns,
  handeRemove: handleRemove,
  handleUpdate,
  setTableData,
}) => {
  const [editCell, setEditCell] = useState({})

  const handleChange = (inputValue, accessor, cellId) => {
    const updatedData = update(tableData, cellId, accessor, inputValue)
    setTableData(updatedData)
  }

  let tData
  return (
    <tbody>
      {tableData.map((data) => {
        return (
          <tr key={data.id} data-key={data.id}>
            {columns.map(({ accessor, editable }) => {
              const isEditing =
                editCell.id === data.id && editCell.accessor === accessor
              tData = data[accessor] ? data[accessor] : isEditing ? '' : '——'

              if (accessor === 'remove') {
                tData = (
                  <button
                    className="button-remove"
                    onClick={(e) => handleRemove(e, data.id)}
                  >
                    X
                  </button>
                )
              } else if (accessor === 'update') {
                tData = (
                  <button
                    data-key={data.id}
                    className="button-update"
                    onClick={(e) => handleUpdate(e, tableData, data.id)}
                    disabled={!data.isEdited}
                  >
                    Update
                  </button>
                )
              }

              return (
                <td
                  key={accessor}
                  onClick={() => setEditCell({ id: data.id, accessor })}
                >
                  {isEditing && editable && accessor !== 'married' ? (
                    <input
                      value={tData}
                      onChange={(e) =>
                        handleChange(e.target.value, accessor, data.id)
                      }
                      onBlur={() => setEditCell({})}
                      autoFocus
                    />
                  ) : isEditing && editable && accessor === 'married' ? (
                    <select
                      onChange={(e) =>
                        handleChange(e.target.value, accessor, data.id)
                      }
                      onBlur={() => setEditCell({})}
                      autoFocus
                      defaultValue={data[accessor]}
                    >
                      <option value={'Yes'}>Yes</option>
                      <option value={'No'}>No</option>
                    </select>
                  ) : (
                    tData
                  )}
                </td>
              )
            })}
          </tr>
        )
      })}
    </tbody>
  )
}

const update = (data = [], editId = 0, fieldName = '', newValue) => {
  const newData = [...data]

  const index = newData.findIndex((d) => d.id === editId)
  newData[index] = { ...newData[index], [fieldName]: newValue }

  // const row = newData.find(d => d.id === editId);
  // row[fieldName] = newValue;

  return newData
}
