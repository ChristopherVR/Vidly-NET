import React from 'react';
import TableHeader from './TableHeader';
import TableBody from './TableBody';
import { Column } from '../../interfaces/column';

function Table({
  columns,
  sortColumn,
  onSort,
  data,
}: {
  columns: Column[];
  data: unknown[];
  onSort: (sortColumn: {
    path: string;
    order: boolean | 'asc' | 'desc';
  }) => void;
  sortColumn: {
    order: boolean | 'asc' | 'desc';
    path: string;
  };
}) {
  return (
    <table className="table table-striped">
      <TableHeader columns={columns} sortColumn={sortColumn} onSort={onSort} />
      <TableBody columns={columns} data={data} />
    </table>
  );
}

export default Table;
