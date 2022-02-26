import React from 'react';
import { Column, SortColumn } from '../../interfaces/column';

function TableHeader({
  columns,
  onSort,
  sortColumn,
}: {
  columns: Column[];
  onSort: (sortCol: SortColumn) => void;
  sortColumn: {
    order: boolean | 'asc' | 'desc';
    path: string;
  };
}) {
  const raiseSort = (path: string) => {
    const changedColumn = { ...sortColumn };
    if (changedColumn.path === path)
      changedColumn.order = sortColumn.order === 'asc' ? 'desc' : 'asc';
    else {
      changedColumn.path = path;
      changedColumn.order = 'asc';
    }
    onSort(changedColumn);
  };

  const renderSortIcon = (column: Column) => {
    if (column.path !== sortColumn.path) return null;
    if (sortColumn.order === 'asc') return <i className="fa fa-sort-asc" />;
    return <i className="fa fa-sort-desc" />;
  };

  return (
    <thead>
      <tr>
        {columns.map((column) => (
          <th
            className="clickable"
            key={column.path || (column.key as string)}
            onClick={() => raiseSort(column.path)}
          >
            {column.label} {renderSortIcon(column)}
          </th>
        ))}
      </tr>
    </thead>
  );
}

export default TableHeader;
