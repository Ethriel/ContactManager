import { useState } from 'react'

export const TableHead = ({ columns, handleSorting }) => {
  const handleSortingChange = (accessor) => {
    const sortOrder = accessor === sortField && order === 'asc' ? 'desc' : 'asc'
    setSortField(accessor)
    setOrder(sortOrder)
    handleSorting?.(accessor, sortOrder)
  }

  const [sortField, setSortField] = useState('')
  const [order, setOrder] = useState('asc')

  return (
    <thead>
      <tr>
        {columns.map(({ label, accessor, sortable }) => {
          const cl = sortable
            ? sortField === accessor && order === 'asc'
              ? 'up'
              : sortField === accessor && order === 'desc'
                ? 'down'
                : 'default'
            : ''
          return (
            <th
              className={cl}
              key={accessor}
              onClick={sortable ? () => handleSortingChange(accessor) : null}
            >
              {label}
            </th>
          )
        })}
      </tr>
    </thead>
  )
}
