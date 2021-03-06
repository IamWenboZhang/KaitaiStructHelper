﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaitai
{
    public partial class EthernetFrame : KaitaiStruct
    {
        public static EthernetFrame FromFile(string fileName)
        {
            return new EthernetFrame(new KaitaiStream(fileName));
        }


        public enum EtherTypeEnum
        {
            Ipv4 = 2048,
            X75Internet = 2049,
            NbsInternet = 2050,
            EcmaInternet = 2051,
            Chaosnet = 2052,
            X25Level3 = 2053,
            Arp = 2054,
            Ipv6 = 34525,
        }
        public EthernetFrame(KaitaiStream p__io, KaitaiStruct p__parent = null, EthernetFrame p__root = null) : base(p__io)
        {
            m_parent = p__parent;
            m_root = p__root ?? this;
            _read();
        }
        private void _read()
        {
            _dstMac = m_io.ReadBytes(6);
            _srcMac = m_io.ReadBytes(6);
            _etherType = ((EtherTypeEnum)m_io.ReadU2be());
            switch (EtherType)
            {
                case EtherTypeEnum.Ipv4:
                    {
                        __raw_body = m_io.ReadBytesFull();
                        var io___raw_body = new KaitaiStream(__raw_body);
                        _body = new Ipv4Packet(io___raw_body);
                        break;
                    }
                case EtherTypeEnum.Ipv6:
                    {
                        __raw_body = m_io.ReadBytesFull();
                        var io___raw_body = new KaitaiStream(__raw_body);
                        _body = new Ipv6Packet(io___raw_body);
                        break;
                    }
                default:
                    {
                        _body = m_io.ReadBytesFull();
                        break;
                    }
            }
        }
        private byte[] _dstMac;
        private byte[] _srcMac;
        private EtherTypeEnum _etherType;
        private object _body;
        private EthernetFrame m_root;
        private KaitaiStruct m_parent;
        private byte[] __raw_body;
        public byte[] DstMac { get { return _dstMac; } }
        public byte[] SrcMac { get { return _srcMac; } }
        public EtherTypeEnum EtherType { get { return _etherType; } }
        public object Body { get { return _body; } }
        public EthernetFrame M_Root { get { return m_root; } }
        public KaitaiStruct M_Parent { get { return m_parent; } }
        public byte[] M_RawBody { get { return __raw_body; } }
    }
}