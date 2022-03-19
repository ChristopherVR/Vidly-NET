import React, { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { MDBValidation, MDBInput, MDBBtn, MDBSwitch } from 'mdb-react-ui-kit';
import { toast } from 'react-toastify';
import { getMovie, saveMovie } from '../services/movieService';
import { Genre } from '../interfaces/genre';
import { Movie } from '../interfaces/movie';
import getGenres from '../services/genreService';

type ParamsProps = {
  id: string;
};

function MovieForm() {
  // eslint-disable-next-line @typescript-eslint/no-unused-vars
  const [genres, setGenres] = useState<Genre[]>([]);
  const [movie, setMovie] = useState<Movie>({
    dailyRentalRate: 0,
    genre: {
      id: 0,
      name: '',
    },
    id: 0,
    liked: false,
    numberInStock: 0,
    rating: 0,
    title: '',
  });
  const navigate = useNavigate();
  const { id } = useParams<ParamsProps>();

  useEffect(() => {
    const populateGenres = async () => {
      try {
        const { data: gen } = await getGenres();
        setGenres(gen);
      } catch {
        console.log('exception boi');
        toast.error('An error occurred trying to retrieve movie genres.');
      }
    };

    const populateMovie = async () => {
      try {
        if (id === 'new') return;

        const { data } = await getMovie(Number(id));
        setMovie(data);
      } catch {
        navigate('/not-found');
      }
    };
    populateGenres();
    populateMovie();
  }, [id, navigate]);

  const onChangeHandler = (ev: HTMLInputElement) => {
    setMovie({
      ...movie,
      [ev.name]: ev.value,
    });
  };

  const doSubmit = async () => {
    await saveMovie(movie);
    navigate('/movies');
  };

  return (
    <div>
      <h1>Movie Form</h1>
      <MDBValidation id="movie-form" onSubmit={doSubmit}>
        <MDBInput
          name="dailyRentalRate"
          label="Daily Rental Rate"
          type="text"
          value={movie.dailyRentalRate.toString()}
          onChange={({ currentTarget }: { currentTarget: HTMLInputElement }) =>
            onChangeHandler(currentTarget)
          }
          required
          invalid
          validation="Daily Rental Rate is required"
        />
        <MDBInput
          name="rating"
          label="Rating"
          type="text"
          value={movie.rating.toString()}
          onChange={({ currentTarget }: { currentTarget: HTMLInputElement }) =>
            onChangeHandler(currentTarget)
          }
          required
          invalid
          validation="Rating is required"
        />
        <MDBInput
          name="title"
          label="Title"
          type="text"
          value={movie.title}
          onChange={({ currentTarget }: { currentTarget: HTMLInputElement }) =>
            onChangeHandler(currentTarget)
          }
          required
          invalid
          validation="Title is required"
        />
        <MDBInput
          name="numberInStock"
          label="Number in Stock"
          type="text"
          value={movie.numberInStock.toString()}
          onChange={({ currentTarget }: { currentTarget: HTMLInputElement }) =>
            onChangeHandler(currentTarget)
          }
          required
          invalid
          validation="Number in Stock is required"
        />
        <MDBSwitch
          value={movie.liked}
          onChange={({ currentTarget }: { currentTarget: HTMLInputElement }) =>
            onChangeHandler(currentTarget)
          }
          label="Liked"
        />
        {/* Add Genre */}
        <MDBBtn form="movie-form" type="submit">
          Submit
        </MDBBtn>
      </MDBValidation>
    </div>
  );
}

export default MovieForm;
