import React, {Component} from 'react';
import { Link } from 'react-router-dom';
import ProductApi from '../api/ProductApi';
import CatalogExportsApi from '../api/CatalogExportsApi';
import Success from './Success';

const Checkbox = () => (
  <i className="fas fa-check"></i>
)

const ApprovePriceButton = (props) => (
  <button type="button" className="btn btn-sm btn-success" onClick={props.approve}>Approve</button>
)

const ListItem = (props) => (
  <tr>
    <td scope="row">{props.product.code}</td>
    <td>{props.product.name}</td>
    <td>{props.product.price}</td>
    <td>{Date(props.product.lastUpdated)}</td>
    <td className="text-center">
    { props.product.priceApproved ? <Checkbox /> : <ApprovePriceButton  approve={props.approve}/>} 
    </td>
    <td className="text-center">
      <Link to={`/products/${props.product.id}`}><button type="button" className="btn btn-sm btn-outline-primary mr-2">View</button></Link>
      <Link to={`/products/${props.product.id}/edit`}><button type="button" className="btn btn-sm btn-warning mr-2">Edit</button></Link>
      <button type="button" className="btn btn-sm btn-outline-danger" onClick={props.delete}>Delete</button>
    </td>
  </tr>
)

class Products extends Component{
  constructor(props){
    super(props);
    this.state = {
      products: [],
      offset: 0,
      limit: 10
    }

    this.handleSearchChanged = this.handleSearchChanged.bind(this);
    this.approveProductPrice = this.approveProductPrice.bind(this);
    this.deleteProduct = this.deleteProduct.bind(this);
    this.handleSearchChanged = this.handleSearchChanged.bind(this);
    this.dismissSuccess = this.dismissSuccess.bind(this);
    this.exportCatalog = this.exportCatalog.bind(this);
    this.prevPage = this.prevPage.bind(this);
    this.nextPage = this.nextPage.bind(this);
    this.search = this.search.bind(this);
  }

  componentWillMount() {
    const search = this.state.search;
    const limit = this.state.limit;
    const offset = this.state.offset;
    this.executeSearch(search, limit, offset);
  }

  handleSearchChanged(event){
    this.setState({search: event.target.value});
  }

  executeSearch(search, limit, offset){
    const request = {
      search: search,
      limit: limit,
      offset: offset
    }

    ProductApi.getProductList(request)
    .then(products => this.setState({products}))
    .catch(error => console.error(error.json()));
  }

  search(){
    const search = this.state.search;
    const limit = this.state.limit;
    const offset = this.state.offset;
    this.executeSearch(search, limit, offset);
  }

  approveProductPrice(id){
    ProductApi.approveProductPrice(id)
    .then(productResponse => this.setState((prevState) => {
      let products = [...prevState.products];
      let index = products.findIndex(x => x.id === productResponse.id);
      products[index] = productResponse;
      return({products})
    }))
    .catch(error => console.error(error.json()));
  }

  deleteProduct(id){
    ProductApi.deleteProduct(id)
    .then(_ => this.setState((prevState) => {
      let products = [...prevState.products];
      let index = products.findIndex(x => x.id === id);
      products.splice(index, 1);
      return({products})
    }))
    .catch(error => console.error(error.json()));
  }

  exportCatalog(){
    CatalogExportsApi.createExport()
    .then(this.setState({success: true}));
  }

  dismissSuccess(){
    this.setState({success: false});
  }

  async nextPage(){
    const search = this.state.search;
    const limit = this.state.limit;
    const offset = this.state.offset + limit;

    this.setState((prevState) => ({offset: prevState.offset + prevState.limit}))
    await this.executeSearch(search, limit, offset);
  }

  async prevPage(){
    const search = this.state.search;
    const limit = this.state.limit;
    const offset = this.state.offset - limit;

    if(this.state.offset > 0){
      this.setState((prevState) => ({offset: prevState.offset - prevState.limit}))
      await this.executeSearch(search, limit, offset);
    }
  }

  render(){
    return(
      <div>
        <div className="my-5">
            <h2>Producs</h2>
        </div>
    
        {this.state.success ? (<Success dismiss={this.dismissSuccess} />): (<div/>)}

        <div className="row">
            <div className="col-3">
                <div className="input-group mb-3">
                    <input type="text" className="form-control" placeholder="Search..." value={this.state.search} onChange={this.handleSearchChanged} />
                    <div className="input-group-append">
                        <button className="btn btn-outline-secondary" type="button" onClick={this.search}>Search</button>
                    </div>
                </div>
            </div>
    
            <div className="col-1">
                <Link to="/products/create"><button type="button" className="btn btn-outline-primary">Create</button></Link>
            </div>
    
            <div className="offset-6 col-2">
                <button type="button" className="btn btn-outline-primary" onClick={this.exportCatalog}>Export Catalog</button>
            </div>
        </div>
    
        <table className="table table-hover">
            <thead>
                <tr>
                    <th scope="col">Code</th>
                    <th scope="col">Name</th>
                    <th scope="col">Price</th>
                    <th scope="col">Last Updated</th>
                    <th className="text-center" scope="col">Approved</th>
                    <th className="text-center" scope="col"></th>
                </tr>
            </thead>
            <tbody>
              {this.state.products.map((product) => <ListItem key={product.id} product={product} approve={() => {this.approveProductPrice(product.id)}} delete={() => {this.deleteProduct(product.id)}} />)}
            </tbody>
        </table>
    
        <div className="row justify-content-md-center">
            <nav aria-label="Product page navigation">
                <ul className="pagination">
                    <li className="page-item"><a className="page-link" onClick={this.prevPage}>Previous</a></li>
                    <li className="page-item"><a className="page-link" onClick={this.nextPage}>Next</a></li>
                </ul>
            </nav>
        </div>
      </div>
    )
  }
} 

export default Products;