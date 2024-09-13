import { useEffect, useState } from 'react';
import './App.css';
import { ApiRoutes } from './constants/api-routes';
import { ImageUploader } from './components/image-uploader';
import axios from 'axios';
import makeRequestAsync from './utilities/make-request-async';

function App() {

    useEffect(() => {
        populateContactsData();
    }, []);

    const [loading, setLoading] = useState(false);
    const [contacts, setContacts] = useState();
    const [selectedFile, setSelectedFile] = useState({});
    const [fileSelected, setFileSelected] = useState(false);

    const onFileChange = event => {
        const file = event.target.files[0];
        setSelectedFile(file);
        setFileSelected(true);
    }

    const onFileUpload = async () => {
        const signal = axios.CancelToken.source();
        try {
            setLoading(true);
            const formData = new FormData();

            formData.set(
                "csv",
                selectedFile,
                selectedFile.name
            );

            const response = await makeRequestAsync(ApiRoutes.uploadCsv, signal.token, formData, "post");
            const apiResult = response.data;
            console.log(apiResult);
            populateContactsData();
        } catch (error) {
            console.log(error);
        }
        finally {
            setLoading(false);
        }
    }
    
    return (
        <div>
            <h1 id="tableLabel">Contacts manager</h1>
            <div>
                <h2>Upload CSV</h2>
                <ImageUploader fileSelected={fileSelected} onFileChange={onFileChange} onFileUpload={onFileUpload} />
            </div>
            <h2 id="tableLabel">Contacts list</h2>

            {loading && <p><em>Loading...</em></p>}
            {(!loading && (contacts === undefined || contacts.length === 0)) && <p><em>No contacts to show</em></p>}
            {!loading && (contacts !== undefined && contacts.length !== 0) &&
                <table className="table table-striped" aria-labelledby="tableLabel">
                    <thead>
                        <tr>
                            <th>Name</th>
                            <th>Date of birth</th>
                            <th>Married</th>
                            <th>Phone</th>
                            <th>Salary</th>
                        </tr>
                    </thead>
                    <tbody>
                        {contacts.map(contact =>
                            <tr key={contact.id} id={'contactId'.concat(contact.id)}>
                                <td>{contact.name}</td>
                                <td>{contact.dateOfBirth}</td>
                                <td>{contact.married ? 'Yes' : 'No'}</td>
                                <td>{contact.phone}</td>
                                <td>{contact.salary}</td>
                            </tr>
                        )}
                    </tbody>
                </table>}
        </div>
    );

    async function populateContactsData() {
        try {
            setLoading(true);
            const signal = axios.CancelToken.source();
            const response = await makeRequestAsync(ApiRoutes.listContacts, signal.token);
            const apiResult = response.data;
            setContacts(apiResult.data);
        } catch (error) {
            console.log(error)
        }
        finally {
            setLoading(false);
        }
    }
}

export default App;