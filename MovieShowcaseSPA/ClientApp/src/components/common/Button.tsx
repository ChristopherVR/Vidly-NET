import React from 'react';

type ButtonProps = {
  disabled: boolean;
  label: string;
};

function Button({ disabled, label }: ButtonProps) {
  return (
    <button type="button" disabled={disabled} className="btn btn-primary">
      {label}
    </button>
  );
}

export default Button;
