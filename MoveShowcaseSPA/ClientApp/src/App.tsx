import React, { lazy, Suspense, useEffect, useMemo, useState } from 'react';
import { Route, Routes } from 'react-router-dom';
import { ToastContainer } from 'react-toastify';
import NotFound from './components/NotFound';
import auth from './services/authService';
import 'react-toastify/dist/ReactToastify.css';
import './App.scss';
import { User } from './interfaces/user';
import UserContext from './context/userContext';
import Loading from './components/Loading';

const Movies = lazy(() => import('./components/Movies'));

const Customers = lazy(() => import('./components/Customers'));

const Rentals = lazy(() => import('./components/Rentals'));

const NavBar = lazy(() => import('./components/NavBar'));

const LoginForm = lazy(() => import('./components/LoginForm'));

const RegisterForm = lazy(() => import('./components/RegisterForm'));

const Logout = lazy(() => import('./components/Logout'));

function App() {
  const [user, setUser] = useState<User>();
  useEffect(() => {
    (async () => {
      const getUser = async () => {
        const response = await auth.getCurrentUser();
        if (response) {
          setUser(response);
        }
      };
      await getUser();
    })();
  }, []);

  const userContextProvider = useMemo(
    () => ({
      user: user ?? ({} as User),
      setUser,
    }),
    [user],
  );

  return (
    <Suspense fallback={<Loading />}>
      <UserContext.Provider value={userContextProvider}>
        <ToastContainer
          position="bottom-left"
          autoClose={5000}
          newestOnTop
          closeOnClick
          pauseOnHover
        />
        <NavBar />
        <main className="container">
          <Routes>
            <Route path="/register" element={<RegisterForm />} />
            <Route path="/login" element={<LoginForm />} />
            <Route path="/logout" element={<Logout />} />
            <Route path="/movies" element={<Movies />} />
            <Route path="/customers" element={<Customers />} />
            <Route path="/rentals" element={<Rentals />} />
            <Route path="*" element={<NotFound />} />
          </Routes>
        </main>
      </UserContext.Provider>
    </Suspense>
  );
}

export default App;