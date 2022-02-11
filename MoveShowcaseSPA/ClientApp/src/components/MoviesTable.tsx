import React from 'react';
import { Link } from 'react-router-dom';
import Table from './common/Table';

import { Movie } from '../interfaces/movie';
import { Column } from '../interfaces/column';
import Like from './common/Like';

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

function MoviesTable({
  movies,
  onSort,
  sortColumn,
  onDelete,
}: {
  onDelete: (movie: Movie) => Promise<void>;
  movies: Movie[];
  onSort: () => void;
  sortColumn: {
    order: boolean | 'asc' | 'desc';
    path: string;
  };
}) {
  return (
    <Table
      data={movies}
      sortColumn={sortColumn}
      onSort={onSort}
      onDelete={onDelete}
    />
  );
}

export default MoviesTable;
