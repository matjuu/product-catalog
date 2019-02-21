import React from 'react';

const Error = (props) => (
  <div className="alert alert-danger col-4 offset-2" role="alert">
    {props.message}
  <button type="button" className="close" onClick={props.dismiss}>
    <span aria-hidden="true">&times;</span>
  </button>
</div>
);

export default Error;