import React from 'react';
import UpdateUserForm from './UpdateUserForm';

function Profile() {
  return (
    <div className="d-flex flex-column">
      <h4>This is the user profile.</h4>
      <div className="mt-4">
        <UpdateUserForm />
      </div>
    </div>
  );
}

export default Profile;
