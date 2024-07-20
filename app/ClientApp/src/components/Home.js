import React, { Component } from 'react';

export class Home extends Component {
  static displayName = Home.name;

  render () {
    return (
      <div>
        <h1>Dueling API</h1>
        <p>This API has the following parts:</p>
        <ul>
          <li>ClientApp: contains the frontend, which is this React app. This has been left to do later.</li>
          <li>Backend API, consisting of Models, Services, Controllers and Tests. The bulk of this assignment.</li>
        </ul>
        <p>To launch and run the React frontend and C# API, open the root folder in Visual Studio Code and press F5. Alternatively, run the backend in a terminal: cd to app/ and run `dotnet start`.</p>
        <p>Send requests to the API using curl statements from the terminal:</p>
        <ul>
          <li>Get By Id: curl https://localhost:5001/character/get/0 (on startup, the app is preseeded with 7 characters with Ids 0 through 6)</li>
          <li>Get All: curl https://localhost:5001/character/get/</li>
          <li>Create: curl -v -d '&#123;\"Name\":\"Jones\", \"Job\":\"Warrior\"&#125;' -H "Content-Type: application/json" https://localhost:5001/character/create</li>
          <li>Battle: curl -v -d '&#123;\"CharacterOneId\":\"0\", \"CharacterTwoId\":\"1\"&#125;' -H "Content-Type: application/json" https://localhost:5001/battle</li>
        </ul>
        <p>To run unit tests, open a terminal and cd to tests, then run `dotnet test`.</p>

      </div>
    );
  }
}
