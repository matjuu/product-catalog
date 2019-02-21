import React, {Component} from 'react';
import ProductApi from '../api/ProductApi';

class ProductView extends Component{

    constructor(props){
        super(props);
        this.state = {
            id: props.match.params.id,
        }
    }

    componentDidMount() {
        ProductApi.getProduct(this.props.match.params.id)
        .then(data => {
            this.setState(data)
        });
    }

    render(){
        return(
        <div>
            <div className="my-5">
                <h2>Product</h2>
            </div>

            <form>
                <div className="row justify-content-md-center">
                    <div className="col-4">
                        <div className="form-group">
                            <label htmlFor="productCode">Code</label>
                            <input type="text" disabled className="form-control" id="code" value={this.state.code}/>
                        </div>
                    </div>
                    <div className="col-4">
                        <div className="form-group">
                            <label htmlFor="productCode">Price</label>
                            <input type="number" disabled step="0.01" className="form-control" id="price" value={this.state.price}/>
                        </div>
                    </div>
                </div>
                <div className="row justify-content-md-center">
                    <div className="col-8">
                        <div className="form-group">
                            <label htmlFor="productCode">Name</label>
                            <input type="text" disabled className="form-control" id="name" value={this.state.name} />
                        </div>
                    </div>
                </div>
                {    
                    this.state.image ?
                    (<div className="justify-content-md-center">
                        <div className="col-8">
                            <img src={`http://localhost:5000/files/images/${this.state.image}`} alt="Image" />
                        </div>
                    </div>) : <div></div>
                }

            </form>
        </div>
        )
    }
}

export default ProductView;