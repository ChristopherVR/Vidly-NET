import React from 'react';

type SearchBoxProps = {
  value: string;
  onChange: (e: string) => void;
};

function SearchBox({ value, onChange }: SearchBoxProps) {
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
