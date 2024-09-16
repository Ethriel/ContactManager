import '../index.css'
import { useRef } from 'react'

export const FileUploader = ({
  onFileChange,
  onFileUpload,
  fileSelected = false,
  multiple = false,
  accept = '.csv',
}) => {
  const inputRef = useRef()
  const resetFile = () => {
    inputRef.current.value = null
  }

  return (
    <>
      <label
        className="file-input"
        htmlFor="inputFile"
        style={{ minWidth: 150, margin: 10 }}
      >
        <input
          id="inputFile"
          onChange={(e) => onFileChange(e, resetFile)}
          type="file"
          style={{ display: 'none' }}
          multiple={multiple}
          ref={inputRef}
          accept={accept}
        />
        Select file
      </label>
      <button
        className="button-upload"
        disabled={!fileSelected}
        onClick={(e) => {
          onFileUpload(e)
        }}
      >
        Upload
      </button>
    </>
  )
}
