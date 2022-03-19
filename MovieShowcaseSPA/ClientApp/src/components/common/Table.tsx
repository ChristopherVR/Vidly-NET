import React from 'react';
import TableHeader from './TableHeader';
import TableBody, { RowData } from './TableBody';
import { Column } from '../../interfaces/column';

type TableProps = {
  columns: Column[];
  data: RowData[];
  onSort: (sortColumn: {
    path: string;
    order: boolean | 'asc' | 'desc';
  }) => void;
  sortColumn: {
    order: boolean | 'asc' | 'desc';
    path: string;
  };
};
function Table({ columns, sortColumn, onSort, data }: TableProps) {
  return (
    <table className="table table-striped">
      <TableHeader columns={columns} sortColumn={sortColumn} onSort={onSort} />
      <TableBody columns={columns} data={data} />
    </table>
  );
}

export default Table;
