import React from 'react';

function SearchBox({
  value,
  onChange,
}: {
  value: string;
  onChange: (e: string) => void;
}) {
  return (
    <input
      type="text"
      name="query"
      className="form-control my-3"
      placeholder="Search..."
      value={value}
      onChange={(e) => onChange(e.currentTarget.value)}
    />
  );
}

export default SearchBox;
