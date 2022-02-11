import React from 'react';

function Button({ disabled, label }: { disabled: boolean; label: string }) {
  return (
    <button type="button" disabled={disabled} className="btn btn-primary">
      {label}
    </button>
  );
}

export default Button;
