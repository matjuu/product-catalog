class ProductApi {
    static apiEndpoint = "http://localhost:5000/api/catalog";

    handleErrors(response) {
        if (!response.ok) {
            throw response;
        }
        return response;
    }

    static getProduct(id){
        let product = fetch(`${this.apiEndpoint}/products/${id}`)
        .then(response => {
            if(!response.ok){
                throw response;
            }
            return response.json();
        })

        return product;
    }

    static getProductList(request){
        let query = `limit=${request.limit}&offset=${request.offset}`;
        if(request.search) query = query + `&search=${request.search}`;

        let products = fetch(`${this.apiEndpoint}/products?${query}`)
        .then(response => {
            if(!response.ok){
                throw response;
            }
            return response.json();
        });

        return products;
    }

    static createProduct(request){
        return fetch(`${this.apiEndpoint}/products/`, 
        {
            method: "POST",
            headers: {'Content-Type': 'application/json'},
            body: JSON.stringify(request)
        })
        .then(response => {
            if(!response.ok){              
                throw response;
            }
            return response.json();
        });
    }

    static updateProduct(id, request){
        return fetch(`${this.apiEndpoint}/products/${id}`, 
        {
            method: "PUT",
            headers: {'Content-Type': 'application/json'},
            body: JSON.stringify(request)
        })
        .then(response => {
            if(!response.ok){              
                throw response;
            }
            return response.json();
        });
    }

    static approveProductPrice(id){
        return fetch(`${this.apiEndpoint}/products/${id}/price/approve`, 
        {
            method: "PUT",
            headers: {'Content-Type': 'application/json'}
        })
        .then(response => {
            if(!response.ok){              
                throw response;
            }
            return response.json();
        });
    }

    static updateProductImage(id, request){
        return fetch(`${this.apiEndpoint}/products/${id}/image`, 
        {
            method: "PUT",
            body: request
        })
        .then(response => {
            if(!response.ok){              
                throw response;
            }
            return response.json();
        });
    }

    static deleteProduct(id){
        return fetch(`${this.apiEndpoint}/products/${id}`, 
        {
            method: "DELETE"
        });
    }
}

export default ProductApi;