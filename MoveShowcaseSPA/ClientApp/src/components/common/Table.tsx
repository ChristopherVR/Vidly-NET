import React from 'react';
import { Link } from 'react-router-dom';
import TableHeader from './tableHeader';
import TableBody from './tableBody';
import Like from './common/like';
import { Movie } from '../../interfaces/movie';
import { Column } from '../../interfaces/column';

const columns = (movie: Movie, onLike: (res: Movie) => Promise<void>) =>
  [
    {
      path: 'title',
      label: 'Title',
      content: (mov: Movie) => (
        <Link to={`/movies/${mov.id}`}>{mov.title}</Link>
      ),
    },
    { path: 'genre.name', label: 'Genre' },
    { path: 'numberInStock', label: 'Stock' },
    { path: 'dailyRentalRate', label: 'Rate' },
    {
      key: 'like',
      content: (mov: Movie) => (
        <Like liked={movie.liked} onClick={() => onLike(mov)} />
      ),
    },
  ] as Column[];

const deleteColumn = (onDelete: (movie: Movie) => Promise<void>) => ({
  key: 'delete',
  content: (movie: Movie) => (
    <button
      type="button"
      onClick={() => onDelete(movie)}
      className="btn btn-danger btn-sm"
    >
      Delete
    </button>
  ),
});

function Table({
  sortColumn,
  onSort,
  data,
  onDelete,
}: {
  data: Movie[];
  onSort: () => void;
  sortColumn: {
    order: boolean | 'asc' | 'desc';
    path: string;
  };
  onDelete: (movie: Movie) => Promise<void>;
}) {
  return (
    <table className="table">
      <TableHeader columns={columns} sortColumn={sortColumn} onSort={onSort} />
      <TableBody columns={columns} data={data} />
    </table>
  );
}

export default Table;
