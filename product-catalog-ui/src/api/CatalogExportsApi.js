class CatalogExportsApi {
    static apiEndpoint = "http://localhost:5000/api/catalog/exports";

    handleErrors(response) {
        if (!response.ok) {
            throw response;
        }
        return response;
    }

    static getExports(request){
        let query = `limit=${request.limit}&offset=${request.offset}`;

        let products = fetch(`${this.apiEndpoint}?${query}`)
        .then(response => {
            if(!response.ok){
                throw response;
            }
            return response.json();
        });

        return products;
    }

    static createExport(request){
        return fetch(`${this.apiEndpoint}`, 
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

    static deleteExport(id){
        return fetch(`${this.apiEndpoint}/${id}`, 
        {
            method: "DELETE"
        });
    }
}

export default CatalogExportsApi;