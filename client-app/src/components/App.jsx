import { Provider } from "react-redux";
import { BrowserRouter } from "react-router-dom";
import BookBox from "../components/BookBox";
import RenderOnAnonymous from "./Guards/RenderOnAnonymous";
import RenderOnAuthenticated from "./Guards/RenderOnAuthenticated";
import Welcome from "./Welcome";

const App = ({ store }) => (
  // <Provider store={store}>
    <BrowserRouter>
      <div className="container">
        <RenderOnAnonymous>
          <Welcome/>
        </RenderOnAnonymous>
        <RenderOnAuthenticated>
          <BookBox/>
        </RenderOnAuthenticated>
      </div>
    </BrowserRouter>
  // </Provider>
);

export default App;
