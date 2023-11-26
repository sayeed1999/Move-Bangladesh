import UserService from "../services/UserService";

const Welcome = () => (
  <div className="my-5 p-5 bg-body-secondary rounded-3">
    <h1 className="text-body-emphasis">Hello Anonymous!</h1>
    <p className="lead">Please authenticate yourself!</p>
    <p>
      <button className="btn btn-lg btn-success" onClick={() => UserService.doLogin()}>Login</button>
    </p>
  </div>
)

export default Welcome
