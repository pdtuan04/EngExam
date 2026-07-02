import { defineConfig } from 'vite';
import plugin from '@vitejs/plugin-react';
import fs from 'fs';
let httpsConfig;

if (
  fs.existsSync('./localhost-key.pem') &&
  fs.existsSync('./localhost.pem')
) {
  httpsConfig = {
    key: fs.readFileSync('./localhost-key.pem'),
    cert: fs.readFileSync('./localhost.pem'),
  };
}

export default defineConfig({
  plugins: [plugin()],
  server: {
    https: httpsConfig,
    port: 5175,
    strictPort: true,
  },
});