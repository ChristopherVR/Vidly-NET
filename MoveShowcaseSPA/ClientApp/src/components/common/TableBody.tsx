/* eslint-disable @typescript-eslint/no-explicit-any */
import React from 'react';
import _ from 'lodash';
import { Column } from '../../interfaces/column';

function TableBody({ data, columns }: { data: any[]; columns: Column[] }) {
  const renderCell = (item: unknown, column: Column) => {
    if (column.content) return column.content(item);

    return _.get(item, column.path);
  };

  const createKey = (item: any, column: Column) =>
    item.id + (column.path || column.key);

  return (
    <tbody>
      {data.map((item) => (
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
