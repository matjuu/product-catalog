import React, {Component} from 'react';
import CatalogExportsApi from '../api/CatalogExportsApi';

const ListItem = (props) => (
    <tr>
      <td>{props.export.name}</td>
      <td>{Date(props.export.createdAt)}</td>
      <td className="text-center">
        <a href={`http://localhost:5000/files/exports/${props.export.fileName}`}><button type="button" className="btn btn-sm btn-outline-primary">Download</button></a>
        <button type="button" className="btn btn-sm btn-outline-danger" onClick={props.delete}>Delete</button>
      </td>
    </tr>
  )

class Exports extends Component {
    constructor(props){
        super(props);
        this.state = {
          exports: [],
          offset: 0,
          limit: 10
        }
    
        this.deleteExport = this.deleteExport.bind(this);
        this.prevPage = this.prevPage.bind(this);
        this.nextPage = this.nextPage.bind(this);
      }

    componentWillMount() {
        const limit = this.state.limit;
        const offset = this.state.offset;
        this.executeSearch(limit, offset);
      }
    
    executeSearch(limit, offset){
        const request = {
            limit: limit,
            offset: offset
        }
    
        CatalogExportsApi.getExports(request)
        .then(exports => this.setState({exports}))
        .catch(error => console.error(error.json()));
    }
    
      deleteExport(id){
        CatalogExportsApi.deleteExport(id)
        .then(_ => this.setState((prevState) => {
          let exports = [...prevState.exports];
          let index = exports.findIndex(x => x.id === id);
          exports.splice(index, 1);
          return({exports})
        }))
        .catch(error => console.error(error.json()));
      }
    
      async nextPage(){
        const limit = this.state.limit;
        const offset = this.state.offset + limit;
    
        this.setState((prevState) => ({offset: prevState.offset + prevState.limit}))
        await this.executeSearch(limit, offset);
      }
    
      async prevPage(){
        const limit = this.state.limit;
        const offset = this.state.offset - limit;
    
        if(this.state.offset > 0){
          this.setState((prevState) => ({offset: prevState.offset - prevState.limit}))
          await this.executeSearch(limit, offset);
        }
      }

    render(){
        return(
            <div>
                <div className="my-5">
                  <h2>Exported Catalogs</h2>
              </div>
              <table className="table table-hover">
                  <thead>
                      <tr>
                          <th scope="col">Name</th>
                          <th scope="col">Created At</th>
                          <th></th>
                      </tr>
                  </thead>
                  <tbody>
                      { this.state.exports.map((exportt) => <ListItem key={exportt.id} export={exportt} delete={() => {this.deleteExport(exportt.id)}} />)}
                  </tbody>
              </table>
          
                  <div className="row justify-content-md-center">
                      <nav aria-label="Product page navigation">
                          <ul className="pagination">
                              <li className="page-item disabled"><a className="page-link" href="#">Previous</a></li>
                              <li className="page-item"><a className="page-link" href="#">Next</a></li>
                          </ul>
                      </nav>
                  </div>
              </div>
          )
    }
} 

export default Exports;