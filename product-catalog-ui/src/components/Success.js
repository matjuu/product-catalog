import React from 'react';

const Success = (props) => (
  <div className="alert alert-success col-4 offset-2" role="alert">
    Action completed successfully!
  <button type="button" className="close" onClick={props.dismiss}>
    <span aria-hidden="true">&times;</span>
  </button>
  </div>
);

export default Success;