import React, { Component } from 'react';
import FileUploader from './FileUploader';
import FileData from './FileData';

export class Home extends Component {
  static displayName = Home.name;

  constructor(props) {
    super(props);
    this.state = { selectedFile: null, loading: true, url: '', files: []};
  }
   
 render() {   
    return (
      <div>    
        <h1 id="tabelLabel" >PDF files</h1>
       
        <FileUploader />
        <FileData/>

      </div>
    );
  }
}
