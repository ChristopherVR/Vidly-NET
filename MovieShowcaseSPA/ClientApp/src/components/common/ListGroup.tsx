import React from 'react';

type ListGroupProps = {
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  items: any[];
  textProperty: string;
  valueProperty: string;
  selectedItem: number;
  onItemSelect: (item: number) => void;
};

function ListGroup({
  items,
  textProperty,
  valueProperty,
  selectedItem,
  onItemSelect,
}: ListGroupProps) {
  return (
    <ul className="list-group">
      {items.map((item) => (
        <li
          aria-hidden
          onClick={() => onItemSelect(Number(item[valueProperty]))}
          key={item[valueProperty]}
          className={
            item[valueProperty] === selectedItem
              ? 'list-group-item active'
              : 'list-group-item'
          }
        >
          {item[textProperty]}
        </li>
      ))}
    </ul>
  );
}

export default ListGroup;
