import React, { useState, useEffect } from "react";

const FileData = () => {
  const [url, setUrl] = useState('');  
  const [files, setFiles] = useState([]);  

  useEffect(() => {
    populateFileData();
 }, []);

  const populateFileData = async () => {   
    const response = await fetch('file/getfiles');
    const data = await response.json();
    setFiles(data);   
  }

  const handleMouseOver = (fileId) => {
    if (fileId) {
        setUrl("file/open/"+fileId);
   }
 };

 const handleClickDownload= async (fileId) => {
    if (fileId) {
        setUrl("file/download/"+fileId);  
    } 
 };

 const handleClickRemove= async (fileId) => {   
    const response = await fetch('file/delete/'+fileId, {method: "DELETE"});
    const data = await response.json();
    populateFileData(); 
  }; 

  return (
    <>
    {files && (
        <table className='table table-stricked' aria-labelledby="tabelLabel">
                <thead>
                <tr>
                    <th col="3">Name</th>
                    <th>Actions</th>
                </tr>
                </thead>
                <tbody>
                {files.map(file =>
                    <tr key={file.id}>
                    <td col="3">{file.name}</td>
                    <td><button type="button" className="btn btn-warning btn-sm" data-bs-toggle="modal" data-bs-target="#exampleModal" onMouseOver={() =>handleMouseOver(file.id)} >
                    Open PDF</button>
                    <button className="btn btn-success btn-sm" onClick={() =>handleClickDownload(file.id)}>Download</button>
                    <button className="btn btn-danger btn-sm" onClick={() =>handleClickRemove(file.id)}>Remove</button></td>              
                    </tr>
                )}
                </tbody>
            </table>
            )}

            <div className="container p-5">
                <div className="modal fade" id="exampleModal" tabIndex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                    <div className="modal-dialog modal-xl">
                        <div className="modal-content" style={{height:"500px"}}>
                        <div className="modal-header">
                            <h5 className="modal-title text-danger" id="exampleModalLabel">PDF</h5>
                            <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div className="modal-body">
                        <iframe title='pdf' src= {url} height="100%" width="100%" ></iframe> 
                        </div>
                        <div className="modal-footer">
                            <button type="button" className="btn btn-warning" data-bs-dismiss="modal">Close</button>
                        </div>
                        </div>
                    </div>
                </div>
            </div>

    </>
  );
};

export default FileData;