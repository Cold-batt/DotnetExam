import * as TabsProps from '@radix-ui/react-tabs';
import { FC } from 'react';

import { TabType } from '@/shared/types';

import { TextBox } from '../TextBox';

import styles from './Tabs.module.scss';

type TabsType = {
  tabs: TabType[];
  currentTab?: string;
  onTabChange?: (value: string) => void;
};

const Tabs: FC<TabsType> = ({ tabs, currentTab, onTabChange }) => {
  return (
    <TabsProps.Root
      className={styles.root}
      onValueChange={onTabChange}
      value={currentTab}
      defaultValue={tabs[0].key}
    >
      <TabsProps.List className={styles.tabList}>
        {tabs.map((tab) => (
          <TabsProps.Trigger className={styles.trigger} value={tab.key} key={tab.key}>
            <TextBox variant='14' color='inherit'>
              {tab.tabName}
            </TextBox>
          </TabsProps.Trigger>
        ))}
      </TabsProps.List>
      {tabs.map((tab) => (
        <TabsProps.Content className={styles.tabContent} value={tab.key} key={tab.key}>
          {tab.tabContent}
        </TabsProps.Content>
      ))}
    </TabsProps.Root>
  );
};

export { Tabs };
