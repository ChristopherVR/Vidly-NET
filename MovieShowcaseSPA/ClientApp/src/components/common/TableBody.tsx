import React from 'react';
import _ from 'lodash';
import { Column } from '../../interfaces/column';

export type RowData = {
  id: number;
  [rest: string]: unknown;
};

type TableBodyProps = {
  data: RowData[];
  columns: Column[];
};
function TableBody({ data, columns }: TableBodyProps) {
  const renderCell = (item: unknown, column: Column) => {
    if (column.content) return column.content(item);

    return _.get(item, column.path);
  };

  const createKey = (item: RowData, column: Column) =>
    `${item.id} ${column.path || column.key}`;

  return (
    <tbody>
      {data.map((item: RowData) => (
        <tr key={item.id}>
          {columns.map((column) => (
            <td key={createKey(item, column)}>{renderCell(item, column)}</td>
          ))}
        </tr>
      ))}
    </tbody>
  );
}

export default TableBody;
