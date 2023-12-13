import React, { useState } from "react";

const FileUploader = () => {
  const [file, setFile] = useState(null);  

  const handleFileChange = (e) => {
     if (e.target.files) {
      setFile(e.target.files[0]);
    }
  };

  const handleUpload = async () => {
    if (file) {

      const formData = new FormData();
      formData.append("file", file);

      try {
        const result = await fetch("file/uploadfile", {
          method: "POST",
          body: formData,
          });

        const data = await result.json();
        
      } catch (error) {
        console.error(error);
      }
    }
  };

  return (
    <>
      <div className="mb-3">
        <label htmlFor="file" className="form-label"> Choose a file</label>
      </div>
      <div className="mb-3">
        <input id="file" className ="form-control" accept="text/html" type="file" onChange={handleFileChange} />
      </div>
     
      {false && file && (
      <div className="mb-3">
        <section>
          File details:
          <ul>
            <li>Name: {file.name}</li>
            <li>Type: {file.type}</li>
            <li>Size: {file.size} bytes</li>
          </ul>         
        </section>  
        </div>      
      )}
      

      {file && (
         <div className="mb-3">
          <button onClick={handleUpload} className="btn btn-primary mb-3">
            Upload a file
          </button>
        </div>
      )}

    </>
  );
};

export default FileUploader;