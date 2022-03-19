import React from 'react';
import { MDBBtn } from 'mdb-react-ui-kit';

import { useNavigate } from 'react-router-dom';
import { Movie } from '../interfaces/movie';
import Like from './common/Like';

type MoviesTableProps = {
  onDelete: (movie: Movie) => Promise<void>;
  movies: Movie[];
  onSort: (sortColumn: { path: string }) => void;
  onLike: (movie: Movie) => void;
};

function MoviesTable({ movies, onSort, onLike, onDelete }: MoviesTableProps) {
  const navgiate = useNavigate();
  return (
    <table className="table table-striped">
      <thead>
        <tr>
          <th
            role="button"
            aria-hidden
            onClick={() =>
              onSort({
                path: 'title',
              })
            }
          >
            Title
          </th>
          <th
            role="button"
            aria-hidden
            onClick={() =>
              onSort({
                path: 'numberInStock',
              })
            }
          >
            Number in Stock
          </th>
          <th
            role="button"
            aria-hidden
            onClick={() =>
              onSort({
                path: 'dailyRentalRate',
              })
            }
          >
            Daily Rental Rate
          </th>
          <th
            role="button"
            aria-hidden
            onClick={() =>
              onSort({
                path: 'rating',
              })
            }
          >
            Rating
          </th>
          <th
            role="button"
            aria-hidden
            onClick={() =>
              onSort({
                path: 'genre',
              })
            }
          >
            Genre
          </th>
          <th
            role="button"
            aria-hidden
            onClick={() =>
              onSort({
                path: 'liked',
              })
            }
          >
            Liked
          </th>
          <th> </th>
        </tr>
      </thead>
      <tbody>
        {movies.map((movie: Movie) => (
          <tr key={movie.id}>
            <td>
              <span
                role="button"
                aria-hidden
                onClick={() => navgiate(`/movies/${movie.id}`)}
              >
                {movie.title}
              </span>
            </td>
            <td>{movie.numberInStock} </td>
            <td>{movie.dailyRentalRate} </td>
            <td>{movie.rating} </td>
            <td>{movie.genre.label} </td>
            <td>
              <Like liked={movie.liked} onClick={() => onLike(movie)} />
            </td>
            <td>
              <MDBBtn className="btn-danger" onClick={() => onDelete(movie)}>
                Delete
              </MDBBtn>
            </td>
          </tr>
        ))}
      </tbody>
    </table>
  );
}

export default MoviesTable;
