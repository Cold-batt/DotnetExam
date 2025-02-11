import react from '@vitejs/plugin-react';
import { URL, fileURLToPath } from 'node:url';
import { defineConfig } from 'vite';
import svgr from 'vite-plugin-svgr';

export default defineConfig({
  resolve: {
    alias: {
      '@': fileURLToPath(new URL('./src', import.meta.url)),
    },
  },
  plugins: [react(), svgr({ include: '**/*.svg?svgr' })],
});
