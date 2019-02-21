import React from 'react';
import { Link } from 'react-router-dom';

const Navigation = () => (
  <div>  
    <ul className="my-3 mr-5 nav nav-pills justify-content-end">
      <li className="nav-item"><Link className="nav-link" to="/">Products</Link></li>
      <li className="nav-item"><Link className="nav-link" to="/exports">Exported Catalogs</Link></li>
    </ul>
  </div>
);

export default Navigation;