/* eslint-disable react/jsx-props-no-spreading */
import React from 'react';

interface InputProps extends React.ComponentPropsWithoutRef<'input'> {
  error?: string;
  label: string;
}
function Input({ name, label, error, ...rest }: InputProps) {
  return (
    <div className="form-group">
      <label htmlFor={name}>{label}</label>
      <input {...rest} name={name} id={name} className="form-control" />
      {error && <div className="alert alert-danger">{error}</div>}
    </div>
  );
}

export default Input;
