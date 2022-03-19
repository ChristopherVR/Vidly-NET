import React, { useState } from 'react';
import { MDBBtn, MDBInput, MDBValidation } from 'mdb-react-ui-kit';
import { RegisterUser, UpdateUser } from '../interfaces/user';

type UserFormProps = {
  user: UpdateUser | RegisterUser;
  submit: (user: UpdateUser | RegisterUser) => Promise<void>;
  title: string;
};

function UserForm({ user, submit, title }: UserFormProps) {
  const [internalUser, setInternalUser] = useState<UpdateUser | RegisterUser>(
    user,
  );

  const onChangeHandler = (ev: HTMLInputElement) => {
    setInternalUser((prevUser) => ({
      ...prevUser,
      [ev.name]: ev.value,
    }));
  };

  return (
    <div>
      <h1>{title}</h1>
      <MDBValidation
        className="mt-2"
        id="user-form"
        onSubmit={async () => submit(internalUser)}
      >
        <MDBInput
          name="userName"
          label="Username"
          type="text"
          className="mb-2"
          value={internalUser.userName}
          onChange={({ currentTarget }: { currentTarget: HTMLInputElement }) =>
            onChangeHandler(currentTarget)
          }
          required
          invalid
          validation="Username is required"
        />
        <MDBInput
          name="password"
          label="Password"
          type="password"
          autoComplete="on"
          className="mb-2"
          value={internalUser.password}
          onChange={({ currentTarget }: { currentTarget: HTMLInputElement }) =>
            onChangeHandler(currentTarget)
          }
          required
          invalid
          validation="Password is required"
        />
        <MDBInput
          name="name"
          label="Name"
          type="text"
          className="mb-2"
          value={internalUser.name}
          onChange={({ currentTarget }: { currentTarget: HTMLInputElement }) =>
            onChangeHandler(currentTarget)
          }
          required
          invalid
          validation="Name is required"
        />
        <MDBInput
          name="surname"
          label="Surname"
          type="text"
          className="mb-2"
          value={internalUser.surname}
          onChange={({ currentTarget }: { currentTarget: HTMLInputElement }) =>
            onChangeHandler(currentTarget)
          }
          required
          invalid
          validation="Surname is required"
        />
        <MDBInput
          name="homeNumber"
          label="Home Number"
          type="tel"
          className="mb-2"
          value={internalUser.homeNumber}
          onChange={({ currentTarget }: { currentTarget: HTMLInputElement }) =>
            onChangeHandler(currentTarget)
          }
          required
          invalid
          validation="Home Number is required"
        />
        <MDBInput
          name="phoneNumber"
          label="Phone Number"
          type="tel"
          className="mb-2"
          value={internalUser.phoneNumber}
          onChange={({ currentTarget }: { currentTarget: HTMLInputElement }) =>
            onChangeHandler(currentTarget)
          }
          required
          invalid
          validation="Phone Number is required"
        />
        <MDBInput
          name="address"
          label="Address"
          type="text"
          className="mb-2"
          value={internalUser.address}
          onChange={({ currentTarget }: { currentTarget: HTMLInputElement }) =>
            onChangeHandler(currentTarget)
          }
          required
          invalid
          validation="Address is required"
        />
        <MDBInput
          name="imageUrl"
          label="Image Url"
          type="url"
          className="mb-2"
          value={internalUser.imageUrl}
          onChange={({ currentTarget }: { currentTarget: HTMLInputElement }) =>
            onChangeHandler(currentTarget)
          }
        />
        <MDBBtn form="user-form" type="submit">
          Submit
        </MDBBtn>
      </MDBValidation>
    </div>
  );
}

export default UserForm;
