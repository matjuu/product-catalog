import React, {Component} from 'react';
import ProductApi from '../api/ProductApi';
import Error from './Error';
import Success from './Success';

class ProductCreate extends Component{

    constructor(props){
        super(props);
        this.state = {
            success: false,
            errorOccured: false,
            code: "",
            price: 0,
            name: ""
        }

        this.handleCodeChanged = this.handleCodeChanged.bind(this);
        this.handlePriceChanged = this.handlePriceChanged.bind(this);
        this.handleNameChanged = this.handleNameChanged.bind(this);
        this.createCustomer = this.createCustomer.bind(this);
        this.dismissSuccess = this.dismissSuccess.bind(this);
        this.dismissError = this.dismissError.bind(this);
        this.handleSelectedFile = this.handleSelectedFile.bind(this);
    }

    async createCustomer(){
        let request = {
            name: this.state.name,
            price: this.state.price,
            code: this.state.code
        };

        await ProductApi.createProduct(request)
        .then(data => this.setState({...data, success: true, errorOccured: false}))
        .catch(error => error.json().then(error => this.setState({error, success: false, errorOccured: true})));

        let id = this.state.id;
        console.log(this.state.uploadFile)
        if(this.state.uploadFile){
            const data = new FormData();
            data.append('image', this.state.selectedFile, this.state.selectedFile.name);
            await ProductApi.updateProductImage(id, data)
            .catch(error => error.json().then(error => this.setState({error, success: false, errorOccured: true})));
        }
    }

    dismissError(){
        this.setState({errorOccured: false});
    }

    dismissSuccess(){
        this.setState({success: false});
    }

    handleSelectedFile(event){
        this.setState({
            selectedFile: event.target.files[0],
            uploadFile: true,
          });
    }

    handleCodeChanged(event){
        this.setState({code: event.target.value})
    }

    handlePriceChanged(event){
        this.setState({price: event.target.value})
    }

    handleNameChanged(event){
        this.setState({name: event.target.value})
    }

    render(){
        const error = this.state.error;
        const errorOccured = this.state.errorOccured;
        const success = this.state.success;
        return(
        <div>
            <div className="my-5">
                <h2>Create new product</h2>
            </div>

            {errorOccured ? (<Error message={error.message} dismiss={this.dismissError}/>) : (<div/>)}
            {success ? (<Success dismiss={this.dismissSuccess} />): (<div/>)}

            <form>
                <div className="row justify-content-md-center">
                    <div className="col-4">
                        <div className="form-group">
                            <label htmlFor="productCode">Code</label>
                            <input type="text" className="form-control" id="code" value={this.state.code} onChange={this.handleCodeChanged}/>
                        </div>
                    </div>
                    <div className="col-4">
                        <div className="form-group">
                            <label htmlFor="productCode">Price</label>
                            <input type="number" step="0.01" className="form-control" id="price" value={this.state.price} onChange={this.handlePriceChanged}/>
                        </div>
                    </div>
                </div>
                <div className="row justify-content-md-center">
                    <div className="col-8">
                        <div className="form-group">
                            <label htmlFor="productCode">Name</label>
                            <input type="text" className="form-control" id="name" value={this.state.name} onChange={this.handleNameChanged}/>
                        </div>
                    </div>
                </div>
                <div className="row justify-content-md-center">
                    <div className="col-8">
                        <div className="form-group">
                            <label htmlFor="productCode">Image</label>
                            <input type="file" className="form-control" id="image" onChange={this.handleSelectedFile} />
                            <small id="fileHelp" className="form-text text-muted">Image is optional</small>
                        </div>
                    </div>
                </div>
                <div className="row">
                    <div className="offset-2 col-1">
                        <button type="button" className="btn btn-primary" onClick={this.createCustomer}>Submit</button>
                    </div>
                </div>

            </form>
        </div>
        )
    }
}

export default ProductCreate;