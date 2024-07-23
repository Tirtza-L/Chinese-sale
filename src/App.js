import { PrimeReactProvider, PrimeReactContext } from 'primereact/api';
import ToolBar2 from './ToolBar';
import Login from './Login';
import { Route } from 'react-router-dom';
import { Routes } from 'react-router-dom';
import { BrowserRouter } from 'react-router-dom';
import Table from './Table';
import RowEditingDemo from './RowEditingDemo';
import TableAllGifts from './TableAllGifts';
import ShoppingCart from './ShoppingCart';
import ShowDonors from './RowEditingDemo';
import Lottery from './Lottery';
import BasicDemo from './report';

function App({ Component, pageProps }) {
  return (
    <PrimeReactProvider>
      <BrowserRouter>
        <ToolBar2 />
        <Routes>
          <Route path="/" element={<Table />} />
          <Route path="/ShowUpdateDonors" element={<ShowDonors />} />
          <Route path="/lottery" element={<Lottery />} />
          <Route path="/Report" element={<BasicDemo />} />
          <Route path="/TableAllGifts" element={<TableAllGifts />} />
          <Route path="/ShoppingCart" element={<ShoppingCart />} />
        </ Routes>
      </BrowserRouter>
    </PrimeReactProvider>
  );
}

export default App;
