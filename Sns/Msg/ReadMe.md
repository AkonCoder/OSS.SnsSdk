��ʹֻ��һ��SDK��ҲҪд��ˮƽ��Ŀ�����㣺1. �����š� 2. ����Ч 

һ. ���ù���
	1. ͨ�����캯�����룬�ʺϵ�һ��Ӧ��
	2. ͨ�� SetContext��ʽ ע�룬�ʺ϶��⻧��ƽ̨�ķ�ʽʹ��
	ʾ�������OSS.SnsSdk.Samples��Ŀ�µ�WxMsgController.cs

��. ���ʹ��

	���ȣ�ϵͳԪ�ؽ��ܣ�����ֱ������ʹ��ģʽ���ٻع�ͷ��������

	1. ʵ�����Ҳ������Ϣ�������Ҫ��Ϊ:
		a. ������Ϣ���̳��� WxBaseRecMsg ����ͨ��Ϣ �� �̳��� WxBaseRecEventMsg ���¼���Ϣ
		    ϵͳĬ��ʵ���� ������ͨ��Ϣ�������¼���Ϣ�����ں�ߵ�ʹ��ģʽ�н��������չ������������

		b. �ظ���Ϣ���̳��� WxBaseReplyMsg ����Ҫ����Ӧ��΢�Žӿڵ�ʵ�壨��ǰ֧������ + WxNoneReplyMsg��
			����������������򷵻���Ϣ�����⼸�����͡���ǰ���ûظ���Ϣ��

			WxTextReplyMsg-�ظ��ı���Ϣ��WxImageReplyMsg-�ظ�ͼƬ��Ϣ��WxVoiceReplyMsg-�ظ�������Ϣ��
			WxVideoReplyMsg-�ظ���Ƶ��Ϣ��WxMusicReplyMsg-�ظ���Ƶ��Ϣ��WxNewsReplyMsg-�ظ�ͼ����Ϣ
			
			WxNoneReplyMsg ��ʾ����Ҫ���Է���Ӧ��ϵͳ�᷵�ظ�΢�Ŷ�success ��
			ʹ���п���ʹ�� WxNoneReplyMsg.None Ĭ��ֵ��

		c. ��Ϣ�����ģ�WxMsgContext ����			
			���� RecMsg �� ReplyMsg �������ԣ�Ҳ�����ϱߵĽ�����Ϣ�ͻظ���Ϣ�����������������п���

	2. Handler����Ϣ��������࣬ʵ��������Ϣ������������ں�ִ�е���
		��ǰϵͳ�� WxMsgBaseHandler �� WxMsgHandler ������ǰ����Ϊ���࣬ʵ�����������ڵĿ��ƺ͵��ȡ�
		������ʵ����ϵͳ������Ϣ���¼����壨������ͨ��Ϣ�¼� �� ���Event��Ϣ�¼���

	3. Procesor��WxMsgProcessor<TRecMsg>������Ϣ�ľ���ִ����.
		���ֻ���ڸ߼�����ģʽ�²Ż���Ҫ�û��Զ���

	
	������ܼ��ֿɹ�ʹ�õ�����ģʽ��

	1. ����ģʽ 
	ϵͳ WxMsgHandler.cs Ĭ��ʵ�ֳ�����������ͨ��Ϣ�������¼���Ϣ��ֻ��Ҫ��д��overwrite����Ӧ���� Process ��ͷ�ķ������ɡ�
	ÿ�������Ĳ�����Ӧ�Ķ�����ϸ����Ϣ���͡����ı�������Ϣ������

    protected override WxBaseReplyMsg ProcessTextMsg(WxTextRecMsg msg)
    {
         return WxNoneReplyMsg.None;
    }

	��ô������д�İ������·�����

	// �����ı���Ϣ
    ProcessTextMsg(WxTextRecMsg msg)

    // ����ͼ����Ϣ
    ProcessImageMsg(WxImageRecMsg msg)

    // ����������Ϣ
    ProcessVoiceMsg(WxVoiceRecMsg msg)

    // ������Ƶ/С��Ƶ��Ϣ
    ProcessVideoMsg(WxVideoRecMsg msg)

    // �������λ����Ϣ
    ProcessLocationMsg(WxLocationRecMsg msg)

    // ����������Ϣ
    ProcessLinkMsg(WxLinkRecMsg msg)

    // �����ע/ȡ����ע�¼�
    ProcessSubscribeEventMsg(WxSubscribeRecEventMsg msg)

    // ����ɨ���������ά���¼�
    ProcessScanEventMsg(WxSubscribeRecEventMsg msg)

    // �����ϱ�����λ���¼�
    ProcessLocationEventMsg(WxLocationRecEventMsg msg)

    // �������˵���ȡ��Ϣʱ���¼�����
    ProcessClickEventMsg(WxClickRecEventMsg msg)

    // �������˵���ת����ʱ���¼�����
    ProcessViewEventMsg(WxViewRecEventMsg msg)


	2. ����ģʽ
		���ڲ��ڻ���ʵ�����͵���Ϣ��ϵͳ�ṩע����Ϣ����ί�е�ģʽ��������Ϣ����һ�� test_msg ��Ϣ����ע��ʾ��
	
	a. ������Ϣʵ�壺
	public class WxTestRecMsg : WxBaseRecMsg
    {
        public string Test { get; set; }
		// ��дʵ��ʵ���ڲ����Ը�ֵ
        protected override void FormatPropertiesFromMsg()
        {
            Test = this["Test"];
        }
    }

	b. ���崦��ί�У�
    static WxTextReplyMsg ProcessTestMsg(WxTestRecMsg msg)
    {
       return new WxTextReplyMsg() { Content = " test_msg ������Ϣ���� " };
    }

	c. ע�루�ں���RegisteEventProcessor��������
	WxMsgProcessorProvider.RegisteProcessor<WxTextRecMsg>("test_msg", ProcessTestMsg);

	��ϲ�����Ѿ�������µ���Ϣ���ʹ���ע�롣

	3.  �߼�ģʽ
		�Զ���Processor������ģʽ�ͽ���ģʽ�ж����ڲ�ʵ����Processor�ĵ��ȣ�������Ȼʹ���ϱ�ʾ����
	
	a. ����ʵ�壨�������ʹ�� WxTestRecMsg��

	b. ����CustomeHandler, ��д��ȡProcessor����
	public class CustomeHandler
	{
      protected override WxMsgProcessor GetCustomProcessor(
		string msgType,string eventName,IDictionary<string, string> msgInfo)
      {
        if (msgType=="test_msg")
        {
            return new WxMsgProcessor<WxTestRecMsg>()
            {
				// ��ί�����������������Ҫ���ͬѧ
				// ���������ͨ�����͵� new t() ����
                RecInsCreater=() => new WxTestRecMsg(),

                // �����¼�����ί��
				// Ҳ����ʹ�������� ProcessTestMsg
				ProcessFunc = msg => WxNoneReplyMsg.None
            };
        }
        return null;
      }
	}
	��ϲ����������˸߼�ģʽ�µĶ��ơ�


��. ����������չ

	�ϱ߽����˼���ģʽ��Ҫʵ�ַ�ʽ����ô��ʵ�ʵ�ʹ�ù��������������Ϣ���ظ��жϣ����ض���Ϣ��ת���ȡ�
	��ϵͳ����Ĳ�ͬ�׶Σ��Ҷ����˼�����Ҫ�Ĵ����¼������������Ϣ����ʱ��ȫ�ֺ;ֲ�����,�ֱ��Ӧ��WxMsgBaseHandler����Execute��ͷ���鷽����

	1. Executing(WxMsgContext context)����ʼִ���¼�����Ϊ��ΧΪȫ����Ϣ���͡�
		���е���Ϣ���Ͷ��ᾭ������¼���Ȼ��ִ�о�����Ϣ���Ͷ�Ӧ��ί��
		��ʱ msgContext �е� ReplyMsg�������context�е� ReplyMsg ���Ը�ֵ���� ��߶���Ķ�Ӧ�ľ�����Ϣί�з���ִ�С�

		������¼������ǿ��Թ����ظ���Ϣ���û���Ȩ��֤��

	2. ExecuteUnknowProcessor(WxBaseRecMsg msg), δ֪��Ϣ�����¼������÷�ΧΪ����δ���ֶ�Ӧ����ί�е���Ϣ���͡�
		��ִ�о�����¼�ʱ�������ǰ��Ϣ����δ���ҵ���Ӧ�Ĵ���ί�У���ỽ���������
		��Ҫע����ǣ���ʹ��ʹ�õ��� WxMsgHandler �����û����д�� ʵ�֣����߷�����Ϊ�գ� Ҳ�ᴥ���˷���

		����ͨ��������������δ֪������Ϣ��־��

	3. ExecuteEnd(WxMsgContext msgContext), ִ�н���ʱ���õķ�������Ϊ��ΧΪȫ����Ϣ���͡�
		������Ϣ����ί��ִ�н������ظ�΢����Ӧ֮ǰ��
		��ʱ msgContext �е� ReplyMsg ��Ϊ�գ����ǰ��ִ�з����з���null����ִ�д˷���֮ǰ����Ĭ�ϸ�ֵ WxNoneReplyMsg.None
		�������������ȫ����־��None���͵���Ϣת���ͷ��ȡ�


��. �������� 

	1. ��ģʽ�����ó���������

	����ģʽ����ģʽ�Ѿ���ϵͳ�ڲ�ʵ�֣�ֻ��Ҫ��дί�з������ɣ��򵥷��㣬�������ô󲿷ֵĳ���

	����ģʽ��ֻ��Ҫ��Ϣ���ͣ�����Ϣ����ί�� �ڳ�����ڴ�ע�ἴ�ɣ�����
	�����ó�������������һ������ж������󣬵���Ϣ���MsgType����Ϊ�գ�΢��ĳЩ�����¼��޷�����
	
	�߼�ģʽ��ʹ�õ�������̳�ģʽ��ÿ�����඼����ʵ��ͬһ��Ϣ�����²�ͬ����ί��
	�����õĳ����� ���⻧ƽ̨�����ÿ����Ϣ���ͣ���ͬ��ƽ̨�ȼ����в�ͬ�����ⶨ��ʵ��
		�Լ������������Ϣ�¼�

	����ģʽ�����ȼ�������ģʽ��ʹ��WxMsgHandlerʱ�� => �߼�ģʽ => ����ģʽ
	�����������ͬʱ�ڸ߼�ģʽ�ͽ���ģʽ�¶�����һ����Ϣ����Ϊ"test"�Ĵ���ʵ�֣�ϵͳĬ��ʹ�ø߼�ģʽ�µ�ʵ�֡�
	�����Ŀ�����ֱ�Ӽ̳��� WxMsgBaseHandler �� ����������ģʽ

	2. �������Եĸ�ֵ����

	������Զ����˽�����Ϣʵ��Ķ�����Ҫ��дFormatPropertiesFromMsg��������� ����ģʽ�� 2.a ��ʵ�֡�
	��Ӧ��Ӧ����Ϣ������Ҫ��ִ��ί����� ToUserName��FromUserName��CreateTime ��ֵ��ϵͳ�Զ�����

	4.  ʹ�÷���ĵط�

	Ϊ�˾����ܼ���ϵͳ�ײ������������Ӱ�죬������ϵͳ�л���û��ʹ�÷�������л����������ط���Ҫע��һ�£�
		1. ��Ҫ�� FormatPropertiesFromMsg �и��Լ������Ը�ֵ��ϵͳ�����ܵ��ṩ��this�������򻯸�ֵ�ķ�ʽ��
		2. �Զ���Processor���̳�WxMsgProcessor<TRecMsg>�������ࣩʱ��RecInsCreater�����������ֵ��
		��ϵͳ�ײ��� ������Ӧ��ʵ��ʱ��ͨ�����͵� new() ����ʵ�֣����ڷ��䡣
		
��. �ռ�����
	ǰ����������������еĶ��������ˣ������������...�㻹��Ҫ����Ķ������ɶȣ���ô������Ҳ�����������㡣
	������������չ����ʵ����һ������������������ܵ�ִ�з��������������������¼�Ҳ���������ﴥ����
	
	ResultMo<WxMsgContext> Execute(string recMsgXml)

	�����ϣ���Լ�����һ���������������ڣ�ok����д���Ｔ�ɡ�ֻ��Ҫ��סһ�����飬�������д����������ļ���ģʽ���������������¼�����Ч��
