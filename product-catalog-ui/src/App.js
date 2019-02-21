import React, { Component } from 'react';
import { BrowserRouter as Router, Route, Switch} from 'react-router-dom';
import Exports from './components/Exports';
import Products from './components/Products';
import Navigation from './components/Navigation';
import ProductCreate from './components/ProductCreate';
import ProductEdit from './components/ProductEdit';
import ProductView from './components/ProductView';



class App extends Component {
  render() {
    return (
      <Router>
        <div>
          <Navigation />
          <div className="container">
              <Route exact path="/" component={Products} />
              <Switch>
                <Route exact path="/products/create" component={ProductCreate} />
                <Route exact path="/products/:id/edit" component={ProductEdit} />
                <Route exact path="/products/:id" component={ProductView} />
              </Switch>
              <Route exact path="/exports" component={Exports} />
          </div>
        </div>
      </Router>
    );
  }
}

export default App;
