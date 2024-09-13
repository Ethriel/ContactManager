/* eslint-disable react/prop-types */
/* eslint-disable no-unused-vars */
import React from 'react';
import '../index.css';

export const ImageUploader = ({ onFileChange, onFileUpload, fileSelected, ...props }) => {
    return (
        <>
        <label
            htmlFor="inputFile"
            style={{ minWidth: 150, margin: 10 }}>
            <input
                id="inputFile"
                onChange={onFileChange}
                type="file"
                style={{ display: "none" }} />
            Select file
        </label>
        <button disabled={!fileSelected} onClick={onFileUpload}>
            Upload
        </button>
        </>
    )
}

//export default ImageUploader;